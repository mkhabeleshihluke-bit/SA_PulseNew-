namespace SA_Pulse
{
    // Defines methods used to validate user input
    internal interface IInputValidator
    {
        int GetValidInteger(string message);
        int GetValidRating(string message);
        string GetValidText(string message);
    }
}