﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Solution.DO.Interfases
{
    public interface ICRUD<T>
    {
        void Insert(T t);
        void Update(T t);
        void Delete(T t);
        IEnumerable<T> GetAll();
        T GetOneById(int id);
    }
}