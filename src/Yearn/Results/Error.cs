namespace Yearn.Results;

public readonly record struct Error
{
    internal static readonly Error None = new(string.Empty, string.Empty);
    public static readonly Error Unknown = new("Error.Unknown", "An unknown or unspecified error has occurred.");
    public static readonly Error NullValue = new("Error.NullValue", "Value cannot be null.");

    private readonly int _codeLength;
    private readonly StringBuffer _codeBuffer;
    
    private readonly int _messageLength;
    private readonly StringBuffer _messageBuffer;
    
    public Error(string Code, string Message)
    {
        _codeLength = Code.Length;
        _codeBuffer = StringBuffer.Create(Code);
        
        _messageLength = Message.Length;
        _messageBuffer = StringBuffer.Create(Message);
    }

    public string Code => _codeBuffer.ToString(_codeLength);
    public string Message => _messageBuffer.ToString(_messageLength);
}

[System.Runtime.CompilerServices.InlineArray(MaxLength)]
internal struct StringBuffer
{
    private const int MaxLength = 150;
    public char Current;
        
    public string ToString(int length)
    {
        if (length > MaxLength)
            throw new Exception();
        
        Span<char> span = stackalloc char[length];
            
        for (var i = 0; i < length; i++)
            span[i] = this[i];
            
        return new string(span);
    }

    public static StringBuffer Create(ReadOnlySpan<char> values)
    {
        if (values.Length > MaxLength)
            throw new Exception();
            
        var codeBuffer = new StringBuffer();
        
        for (var i = 0; i < values.Length; i++)
            codeBuffer[i] = values[i];

        return codeBuffer;
    }
}