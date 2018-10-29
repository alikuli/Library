using ConfigManagerLibrary;
using ConstantsLibrary;
using EnumLibrary.EnumNS;
using ModelsClassLibrary.ModelsNS.VerificatonNS;
using System;

namespace UowLibrary
{
    /// <summary>
    /// This is where all the Fix, bussiness Rules and ErrorChecks are implemented
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract partial class BusinessLayer<TEntity>
    {
        public VerificationIconResult GetVerificationIcon(VerificaionStatusENUM verificationStatus)
        {
            VerificationIconResult result = new VerificationIconResult();

            switch (verificationStatus)
            {
                case VerificaionStatusENUM.Unknown:
                case VerificaionStatusENUM.NotVerified:
                    result.IconAddress = VerificationConfig.NotVerifiedIcon;
                    result.ToolTip = VerificationToolTipConstants.NOTVERIFIED_ICON;
                    return result;

                case VerificaionStatusENUM.Failed:
                    result.IconAddress = VerificationConfig.FailedIcon;
                    result.ToolTip = VerificationToolTipConstants.FAILED_ICON;
                    return result;

                case VerificaionStatusENUM.Printed:
                    result.IconAddress = VerificationConfig.PrintedIcon;
                    result.ToolTip = VerificationToolTipConstants.PRINTED_ICON;
                    return result;

                case VerificaionStatusENUM.Mailed:
                    result.IconAddress = VerificationConfig.MailedIcon;
                    result.ToolTip = VerificationToolTipConstants.MAILED_ICON;
                    return result;

                case VerificaionStatusENUM.Requested:
                    result.IconAddress = VerificationConfig.RequestIconIcon;
                    result.ToolTip = VerificationToolTipConstants.REQUESTED_ICON;
                    return result;

                case VerificaionStatusENUM.SelectedForProcessing:
                    result.IconAddress = VerificationConfig.InproccessIcon;
                    result.ToolTip = VerificationToolTipConstants.INPROCCESS_ICON;
                    return result;

                case VerificaionStatusENUM.Verified:
                    result.IconAddress = VerificationConfig.VerifiedIcon;
                    result.ToolTip = VerificationToolTipConstants.VERIFIED_ICON;
                    return result;

                default:
                    result.IconAddress = VerificationConfig.NotVerifiedIcon;
                    result.ToolTip = VerificationConfig.NotVerifiedIcon;
                    return result;
            }
        }

    }
}
