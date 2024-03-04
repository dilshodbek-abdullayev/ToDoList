using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Domain.Entities.Enums
{
    public enum Permission
    {
        CreateUser=1,
        GetAll=2,
        GetByUserId=3,
        GetByUserEmail=4,
        GetByRole=5,
        GetByUserName=6,
        UpdateUser,
        DeleteUser,
        AddNotePad,
        UpdateNotePad,
        DeleteNotePad,
        GetUserPdf
    }
}
