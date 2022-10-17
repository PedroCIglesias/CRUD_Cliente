using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ImoveisClienteMVC.Models;

namespace ImoveisClienteMVC.Data
{
    public class ImoveisClienteMVCContext : DbContext
    {
        public ImoveisClienteMVCContext (DbContextOptions<ImoveisClienteMVCContext> options)
            : base(options)
        {
        }

        public DbSet<ImoveisClienteMVC.Models.ClienteViewModel> ClienteViewModel { get; set; } = default!;
    }
}
