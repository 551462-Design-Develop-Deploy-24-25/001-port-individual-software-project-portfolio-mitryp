namespace ACW1.Core.XML.Exceptions;

public class DecodeException(string typeName) : Exception($"Could not decode {typeName}");
