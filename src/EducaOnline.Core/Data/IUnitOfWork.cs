﻿namespace EducaOnline.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
