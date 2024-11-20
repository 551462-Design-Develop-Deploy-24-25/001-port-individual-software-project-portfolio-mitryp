namespace ACW1.Core.XML.Exceptions;

public class CollectionValidationException(string collectionName, string message)
    : Exception($"Validation for collection {collectionName} failed: {message}");
