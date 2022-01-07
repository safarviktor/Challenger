using System.ComponentModel;

namespace Challenger.Models
{
    public enum MessageType
    {
        [Description("The confirm password is not the same as the main password")]
        ConfirmPasswordIncorrect,

        [Description("The selected email address already exists")]
        UserEmailAlreadyExists,

        [Description("The supplied credentials are invalid")]
        InvalidCredentials
    }
}