namespace Noty.Shared.FileOperations
{
    public interface IFileIdentifier
    {
        IFileService IdentifyFileExtension(IDialogService dialog);
    }
}
