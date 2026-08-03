using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Operix.Infrastructure.Data;

public class OperixDbContext : DbContext
{
    public OperixDbContext(DbContextOptions<OperixDbContext> options): base(options)
    {
        
    }
}