/*	
 * 	
 *  This file is part of libfintx.
 *  
 *  Copyright (C) 2016 - 2022 Torsten Klinger
 * 	E-Mail: torsten.klinger@googlemail.com
 *  
 *  This program is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program; if not, write to the Free Software Foundation,
 *  Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 * 	
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libfintx.FinTS.Data;
using libfintx.FinTS.Exceptions;
using libfintx.FinTS.Message;
using libfintx.Globals;
using Microsoft.Extensions.Logging;

namespace libfintx.FinTS
{
    public static class INI
    {
        /// <summary>
        /// INI
        /// </summary>
        public static async Task<string> Init_INI(FinTsClient client, string? hkTanSegmentId = null)
        {
            SEG sEG = new SEG();
            StringBuilder sb = new StringBuilder();

            var connectionDetails = client.ConnectionDetails;
            if (!client.Anonymous)
            {
                // Sync
                string segments;

                // INI
                if (connectionDetails.FinTSVersion == FinTsVersion.v220)
                {
                    sb = new StringBuilder();
                    sb.Append("HKIDN");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg3);
                    sb.Append(DEG.Separator);
                    sb.Append("2");
                    sb.Append(sEG.Delimiter);
                    sb.Append(SEG_COUNTRY.Germany);
                    sb.Append(DEG.Separator);
                    sb.Append(connectionDetails.BlzPrimary);
                    sb.Append(sEG.Delimiter);
                    sb.Append(connectionDetails.UserIdEscaped);
                    sb.Append(sEG.Delimiter);
                    sb.Append(client.SystemId);
                    sb.Append(sEG.Delimiter);
                    sb.Append("1");
                    sb.Append(sEG.Terminator);

                    sb.Append("HKVVB");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg4);
                    sb.Append(DEG.Separator);
                    sb.Append("2");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.ProductId);
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.Version);
                    sb.Append(sEG.Terminator);

                    string segments_ = sb.ToString();

                    // string segments_ =
                    //    "HKIDN:" + SEG_NUM.Seg3 + ":2+" + SEG_COUNTRY.Germany + ":" + connectionDetails.BlzPrimary + "+" + connectionDetails.UserId + "+" + client.SystemId + "+1'" +
                    //    "HKVVB:" + SEG_NUM.Seg4 + ":2+0+0+0+" + FinTsGlobals.ProductId + "+" + FinTsGlobals.Version + "'";

                    segments = segments_;
                }
                else if (connectionDetails.FinTSVersion == FinTsVersion.v300)
                {
                    sb = new StringBuilder();
                    sb.Append("HKIDN");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg3);
                    sb.Append(DEG.Separator);
                    sb.Append("2");
                    sb.Append(sEG.Delimiter);
                    sb.Append(SEG_COUNTRY.Germany);
                    sb.Append(DEG.Separator);
                    sb.Append(connectionDetails.BlzPrimary);
                    sb.Append(sEG.Delimiter);
                    sb.Append(connectionDetails.UserIdEscaped);
                    sb.Append(sEG.Delimiter);
                    sb.Append(client.SystemId);
                    sb.Append(sEG.Delimiter);
                    sb.Append("1");
                    sb.Append(sEG.Terminator);

                    sb.Append("HKVVB");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg4);
                    sb.Append(DEG.Separator);
                    sb.Append("3");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.ProductId);
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.Version);
                    sb.Append(sEG.Terminator);

                    string segments_ = sb.ToString();

                    // string segments_ =
                    //    "HKIDN:" + SEG_NUM.Seg3 + ":2+" + SEG_COUNTRY.Germany + ":" + connectionDetails.BlzPrimary + "+" + connectionDetails.UserId + "+" + client.SystemId + "+1'" +
                    //    "HKVVB:" + SEG_NUM.Seg4 + ":3+0+0+0+" + FinTsGlobals.ProductId + "+" + FinTsGlobals.Version + "'";

                    if (client.HITANS >= 6)
                    {
                        client.SEGNUM = Convert.ToInt16(SEG_NUM.Seg5);
                        segments_ = HKTAN.Init_HKTAN(client, segments_, hkTanSegmentId);
                    }
                    else
                    {
                        client.SEGNUM = Convert.ToInt16(SEG_NUM.Seg4);
                    }

                    segments = segments_;
                }
                else
                {
                    throw new FinTsVersionNotSupportedException(connectionDetails.FinTSVersion,
                        new[] { FinTsVersion.v220, FinTsVersion.v300 });
                }

                var message = FinTSMessage.Create(client, 1, "0", segments, client.HIRMS);
                var response = await FinTSMessage.Send(client, message);

                try
                {
                    client.Parse_Segments(response)
                        .ToList();
                }
                catch (Exception ex)
                {
                    client.Logger.LogError(ex, ex.ToString());

                    throw new InvalidOperationException($"Software error: {ex.Message}", ex);
                }

                return response;
            }
            else
            {
                // Sync
                client.Logger.LogInformation("Starting Synchronisation anonymous");

                string segments;

                if (connectionDetails.FinTSVersion == FinTsVersion.v300)
                {
                    sb = new StringBuilder();
                    sb.Append("HKIDN");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg3);
                    sb.Append(DEG.Separator);
                    sb.Append("2");
                    sb.Append(sEG.Delimiter);
                    sb.Append(SEG_COUNTRY.Germany);
                    sb.Append(DEG.Separator);
                    sb.Append(connectionDetails.BlzPrimary);
                    sb.Append(sEG.Delimiter);
                    sb.Append("9999999999");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Terminator);

                    sb.Append("HKVVB");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg4);
                    sb.Append(DEG.Separator);
                    sb.Append("3");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("1");
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.ProductId);
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.Version);
                    sb.Append(sEG.Terminator);

                    string segments_ = sb.ToString();

                    // string segments_ =
                    //    "HKIDN:" + SEG_NUM.Seg2 + ":2+" + SEG_COUNTRY.Germany + ":" + connectionDetails.BlzPrimary + "+" + "9999999999" + "+0+0'" +
                    //    "HKVVB:" + SEG_NUM.Seg3 + ":3+0+0+1+" + FinTsGlobals.ProductId + "+" + FinTsGlobals.Version + "'";

                    segments = segments_;
                }
                else
                {
                    throw new FinTsVersionNotSupportedException(connectionDetails.FinTSVersion,
                        new[] { FinTsVersion.v300 });
                }

                client.SEGNUM = Convert.ToInt16(SEG_NUM.Seg4);

                string message = FinTsMessageAnonymous.Create(connectionDetails.HbciVersion, "1", "0", connectionDetails.Blz, connectionDetails.UserIdEscaped, connectionDetails.Pin, "0", segments, null, client.SEGNUM);
                string response = await FinTSMessage.Send(client, message);

                IEnumerable<HBCIBankMessage> messages;
                try
                {
                    messages = client.Parse_Segments(response)
                        .ToList();
                }
                catch (Exception ex)
                {
                    client.Logger.LogError(ex, ex.ToString());

                    throw new InvalidOperationException($"Software error: {ex.Message}", ex);
                }

                var result = new HBCIDialogResult(messages, response);
                if (!result.IsSuccess)
                {
                    // If anonymous BPD failed but we already have a SystemId from a previous
                    // synchronization, fall back to the authenticated (non-anonymous) INI path.
                    // This handles banks (e.g. Berliner Sparkasse) that reject anonymous BPD
                    // with "9010: Nachricht ungueltig".
                    if (client.SystemId != null && client.SystemId != "0")
                    {
                        client.Logger.LogInformation("Synchronisation anonymous failed, but SystemId is available ({SystemId}). Falling back to authenticated INI.", client.SystemId);
                        return await Init_INI_Authenticated(client, hkTanSegmentId);
                    }

                    client.Logger.LogInformation("Synchronisation anonymous failed. " + result);
                    return response;
                }

                // Sync OK
                client.Logger.LogInformation("Synchronisation anonymous ok");

                // INI
                if (connectionDetails.FinTSVersion == FinTsVersion.v300)
                {
                    sb = new StringBuilder();
                    sb.Append("HKIDN");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg3);
                    sb.Append(DEG.Separator);
                    sb.Append("2");
                    sb.Append(sEG.Delimiter);
                    sb.Append(SEG_COUNTRY.Germany);
                    sb.Append(DEG.Separator);
                    sb.Append(connectionDetails.BlzPrimary);
                    sb.Append(sEG.Delimiter);
                    sb.Append(connectionDetails.UserIdEscaped);
                    sb.Append(sEG.Delimiter);
                    sb.Append(client.SystemId);
                    sb.Append(sEG.Delimiter);
                    sb.Append("1");
                    sb.Append(sEG.Terminator);

                    sb.Append("HKVVB");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg4);
                    sb.Append(DEG.Separator);
                    sb.Append("3");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.ProductId);
                    sb.Append(sEG.Delimiter);
                    sb.Append(FinTsGlobals.Version);
                    sb.Append(sEG.Terminator);

                    sb.Append("HKSYN");
                    sb.Append(DEG.Separator);
                    sb.Append(SEG_NUM.Seg4);
                    sb.Append(DEG.Separator);
                    sb.Append("3");
                    sb.Append(sEG.Delimiter);
                    sb.Append("0");
                    sb.Append(sEG.Terminator);

                    string segments__ = sb.ToString();

                    // string segments__ =
                    //    "HKIDN:" + SEG_NUM.Seg3 + ":2+" + SEG_COUNTRY.Germany + ":" + connectionDetails.BlzPrimary + "+" + connectionDetails.UserId + "+" + client.SystemId + "+1'" +
                    //    "HKVVB:" + SEG_NUM.Seg4 + ":3+0+0+0+" + FinTsGlobals.ProductId + "+" + FinTsGlobals.Version + "'" +
                    //    "HKSYN:" + SEG_NUM.Seg5 + ":3+0'";

                    segments = segments__;
                }
                else
                {
                    throw new FinTsVersionNotSupportedException(connectionDetails.FinTSVersion,
                        new[] { FinTsVersion.v300 });
                }

                client.SEGNUM = Convert.ToInt16(SEG_NUM.Seg5);

                message = FinTSMessage.Create(client, 1, "0", segments, client.HIRMS);
                response = await FinTSMessage.Send(client, message);

                try
                {
                    client.Parse_Segments(response)
                        .ToList();
                }
                catch (Exception ex)
                {
                    client.Logger.LogError(ex, ex.ToString());

                    throw new InvalidOperationException($"Software error: {ex.Message}", ex);
                }

                return response;
            }
        }

        /// <summary>
        /// Authenticated INI path — used as fallback when anonymous BPD fails
        /// but the client already has a valid SystemId.
        /// </summary>
        private static async Task<string> Init_INI_Authenticated(FinTsClient client, string? hkTanSegmentId)
        {
            SEG sEG = new SEG();
            StringBuilder sb = new StringBuilder();
            var connectionDetails = client.ConnectionDetails;
            string segments;

            if (connectionDetails.FinTSVersion == FinTsVersion.v220)
            {
                sb = new StringBuilder();
                sb.Append("HKIDN");
                sb.Append(DEG.Separator);
                sb.Append(SEG_NUM.Seg3);
                sb.Append(DEG.Separator);
                sb.Append("2");
                sb.Append(sEG.Delimiter);
                sb.Append(SEG_COUNTRY.Germany);
                sb.Append(DEG.Separator);
                sb.Append(connectionDetails.BlzPrimary);
                sb.Append(sEG.Delimiter);
                sb.Append(connectionDetails.UserIdEscaped);
                sb.Append(sEG.Delimiter);
                sb.Append(client.SystemId);
                sb.Append(sEG.Delimiter);
                sb.Append("1");
                sb.Append(sEG.Terminator);

                sb.Append("HKVVB");
                sb.Append(DEG.Separator);
                sb.Append(SEG_NUM.Seg4);
                sb.Append(DEG.Separator);
                sb.Append("2");
                sb.Append(sEG.Delimiter);
                sb.Append("0");
                sb.Append(sEG.Delimiter);
                sb.Append("0");
                sb.Append(sEG.Delimiter);
                sb.Append("0");
                sb.Append(sEG.Delimiter);
                sb.Append(FinTsGlobals.ProductId);
                sb.Append(sEG.Delimiter);
                sb.Append(FinTsGlobals.Version);
                sb.Append(sEG.Terminator);

                segments = sb.ToString();
            }
            else if (connectionDetails.FinTSVersion == FinTsVersion.v300)
            {
                sb = new StringBuilder();
                sb.Append("HKIDN");
                sb.Append(DEG.Separator);
                sb.Append(SEG_NUM.Seg3);
                sb.Append(DEG.Separator);
                sb.Append("2");
                sb.Append(sEG.Delimiter);
                sb.Append(SEG_COUNTRY.Germany);
                sb.Append(DEG.Separator);
                sb.Append(connectionDetails.BlzPrimary);
                sb.Append(sEG.Delimiter);
                sb.Append(connectionDetails.UserIdEscaped);
                sb.Append(sEG.Delimiter);
                sb.Append(client.SystemId);
                sb.Append(sEG.Delimiter);
                sb.Append("1");
                sb.Append(sEG.Terminator);

                sb.Append("HKVVB");
                sb.Append(DEG.Separator);
                sb.Append(SEG_NUM.Seg4);
                sb.Append(DEG.Separator);
                sb.Append("3");
                sb.Append(sEG.Delimiter);
                sb.Append("0");
                sb.Append(sEG.Delimiter);
                sb.Append("0");
                sb.Append(sEG.Delimiter);
                sb.Append("0");
                sb.Append(sEG.Delimiter);
                sb.Append(FinTsGlobals.ProductId);
                sb.Append(sEG.Delimiter);
                sb.Append(FinTsGlobals.Version);
                sb.Append(sEG.Terminator);

                string segments_ = sb.ToString();

                if (client.HITANS >= 6)
                {
                    client.SEGNUM = Convert.ToInt16(SEG_NUM.Seg5);
                    segments_ = HKTAN.Init_HKTAN(client, segments_, hkTanSegmentId);
                }
                else
                {
                    client.SEGNUM = Convert.ToInt16(SEG_NUM.Seg4);
                }

                segments = segments_;
            }
            else
            {
                throw new FinTsVersionNotSupportedException(connectionDetails.FinTSVersion,
                    new[] { FinTsVersion.v220, FinTsVersion.v300 });
            }

            // Use PIN:2+HKTAN6 for Init when HIRMS is set (PSD2-compliant banks like
            // Berliner Sparkasse). Previously we cleared HIRMS to force PIN:1, but
            // PSD2-strict banks require HKTAN6 in the Init dialog — without it they
            // return "9075: Banking-Programm nicht PSD2-fähig". With HKTAN6 included
            // (thanks to HITANS >= 6 set after Sync), we use a consistent PIN:2
            // security profile for the entire dialog (Init + HKSAL).
            var message = FinTSMessage.Create(client, 1, "0", segments, client.HIRMS);
            var response = await FinTSMessage.Send(client, message);

            try
            {
                client.Parse_Segments(response)
                    .ToList();
            }
            catch (Exception ex)
            {
                client.Logger.LogError(ex, ex.ToString());

                throw new InvalidOperationException($"Software error: {ex.Message}", ex);
            }

            // Parse_Segments may update HIRMS from the bank's "3920" response.
            // We keep it — the entire dialog uses consistent PIN:2+TAN security.

            return response;
        }
    }
}
