using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trinity.Mvc.Domain;

namespace Trinity.Mvc.Data.Repository
{
    public interface ILikeRepository
    {
        bool LikeExist(long objectId, LikableType type, string userId);
    }
}