# Orso.Arpa.FakeSmtp

## Abstract

This project is only relevant for a development setting. It starts a [fake SMTP server](http://nilhcem.com/FakeSMTP/) on port 2500. If you run the app locally just start this project as well to use the fake SMTP server which provides a UI to display the sent e-mail messages.

*Hint*: Please stop the project using either `CTRL C` or by closing the SMTP server UI. If you stop the project using the stop debugging button in Visual Studio, the SMTP server won't be stopped and the mails folder will not be cleared. You will then have to do this manually.
