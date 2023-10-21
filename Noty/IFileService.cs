using System.Collections.Generic;
 
namespace Noty
{
    public interface IFileService<T>
    {
        List<T> Open(string filename);
        void Save(string filename, List<T> phonesList);
    }
}