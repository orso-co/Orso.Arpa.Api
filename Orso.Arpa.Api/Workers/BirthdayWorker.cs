using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orso.Arpa.Domain.PersonDomain.Commands;
using Orso.Arpa.Domain.PersonDomain.Model;
using Orso.Arpa.Domain.PersonDomain.Queries;
using Orso.Arpa.Misc;
using Orso.Arpa.Misc.Extensions;
using Orso.Arpa.Misc.Logging;

namespace Orso.Arpa.Api.Workers;

public sealed class BirthdayWorker : BackgroundService
{
    private readonly ILogger<BirthdayWorker> _logger;
    private readonly IServiceProvider _serviceProvider;

    private readonly IDateTimeProvider _dateTimeProvider;

    private const string LoggerPrefix = "BIRTHDAY_WORKER:";


    public BirthdayWorker(ILogger<BirthdayWorker> logger, IServiceProvider serviceProvider, IDateTimeProvider dateTimeProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Prefix} Worker says hello", LoggerPrefix);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                DateTime currentTimeInBerlin = GetCurrentTimeInBerlin();
                DateTime midnightInBerlin = currentTimeInBerlin.GetNextMidnight();
                TimeSpan waitTime = midnightInBerlin - currentTimeInBerlin;

                _logger.LogInformation("{Prefix} Remaining time to next worker run {WaitTime} hours", LoggerPrefix, waitTime.TotalHours.ToString("N2"));

                await Task.Delay(waitTime, stoppingToken);

                using IServiceScope scope = _serviceProvider.CreateScope();
                IMediator mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                IList<Person> persons = (await mediator.Send(new ListBirthdayChildren.Query { Date = midnightInBerlin }, stoppingToken)) ?? [];

                foreach (Person person in persons)
                {
                    try
                    {
                        var emailAddress = person.GetPreferredEMailAddress();
                        var name = person.DisplayName;

                        if (emailAddress is null)
                        {
                            LogError(name);
                            continue;
                        }

                        await SendEMailAsync(currentTimeInBerlin, mediator, name, emailAddress, stoppingToken);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "{Prefix} An error occured while sending birthday e-mail for {Person}", LoggerPrefix, person);
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Prefix} An error occured while executing BirthdayWorker", LoggerPrefix);
            }
        }
    }

    private async Task SendEMailAsync(DateTime currentTimeInBerlin, IMediator mediator, string personName, string emailAddress, CancellationToken stoppingToken)
    {
        _logger.LogInformation("{Prefix} Sending birthday e-mail to {Person}", LoggerPrefix, personName);
        await mediator.Send(new SendBirthdayMail.Command { RecipientEMailAddress = emailAddress, RecipientName = personName }, stoppingToken);
        KbbInfoLogger.LogInfoForKbb(
            _logger,
            "BIRTHDAY E-MAIL SENT",
            new Dictionary<string, object>
            {
                { "Name", personName },
                { "Date", currentTimeInBerlin.ToShortDateString() }
            },
            "Birthday e-mail successfully sent");
    }


    private void LogError(string personName)
    {
        _logger.LogWarning("{Prefix} Couldn't send birthday e-mail to {Person} due to missing e-mail address", LoggerPrefix, personName);
        KbbInfoLogger.LogInfoForKbb(
            _logger,
            "BIRTHDAY E-MAIL ERROR",
            new Dictionary<string, object>
            {
                { "Name", personName },
                { "Error", "Couldn't send birthday e-mail; No e-mail address found for this person" }
            },
            "An error occured");
    }


    private DateTime GetCurrentTimeInBerlin()
    {
        // Define the Berlin timezone
        TimeZoneInfo berlinTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

        // Get the current time in Berlin timezone
        return TimeZoneInfo.ConvertTimeFromUtc(_dateTimeProvider.GetUtcNow(), berlinTimeZone);
    }
}
