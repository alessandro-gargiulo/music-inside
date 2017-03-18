﻿using MusicInside.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicInside.DataAccessInterfaces
{
    public interface IGenreDataAccess
    {
        List<Genre> GetGenresBySongId(int id);
    }
}