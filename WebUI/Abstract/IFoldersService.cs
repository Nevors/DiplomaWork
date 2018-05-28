using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Abstract {
    public interface IFoldersService {
        ICollection<Folder> Folders { get; }
    }
}
