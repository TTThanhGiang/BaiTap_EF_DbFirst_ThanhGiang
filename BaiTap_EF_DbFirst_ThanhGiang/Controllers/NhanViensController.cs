using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BaiTap_EF_DbFirst_ThanhGiang.Models;

namespace BaiTap_EF_DbFirst_ThanhGiang.Controllers
{
    public class NhanViensController : Controller
    {
        private readonly QuanlynhanvienContext _context;

        public NhanViensController(QuanlynhanvienContext context)
        {
            _context = context;
        }

        // GET: NhanViens
        public IActionResult Index()
        {
            var quanlynhanvienContext = _context.NhanViens
                                         .Include(n => n.MaPhongBanNavigation)
                                         .Where(n => n.GioiTinh.Contains("Nam")
                                         && (DateOnly.FromDateTime(DateTime.Now).Year - n.NgaySinh.Value.Year) > 30 
                                         && (DateOnly.FromDateTime(DateTime.Now).Year - n.NgaySinh.Value.Year) < 40
                                         && n.MaPhongBanNavigation.TenPhongBan.Contains("Marketing")).ToList();
            List<NhanVienViewModel> list = new List<NhanVienViewModel>();
            foreach (var item in quanlynhanvienContext)
            {
                var listnhanvien = new NhanVienViewModel()
                {
                    NhanVienId = item.MaNhanVien,
                    Ten = item.TenNhanVien,
                    GioiTinh = item.GioiTinh,
                    NgaySinh = (DateOnly)item.NgaySinh,
                    tenphong = item.MaPhongBanNavigation.TenPhongBan

                };
                list.Add(listnhanvien);
            }




            return View(list);
        }

        // GET: NhanViens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // GET: NhanViens/Create
        public IActionResult Create()
        {
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "MaPhongBan");
            return View();
        }

        // POST: NhanViens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaNhanVien,TenNhanVien,NgaySinh,GioiTinh,MaPhongBan")] NhanVien nhanVien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanVien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "MaPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien == null)
            {
                return NotFound();
            }
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "MaPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // POST: NhanViens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaNhanVien,TenNhanVien,NgaySinh,GioiTinh,MaPhongBan")] NhanVien nhanVien)
        {
            if (id != nhanVien.MaNhanVien)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanVien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanVienExists(nhanVien.MaNhanVien))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhongBan"] = new SelectList(_context.PhongBans, "MaPhongBan", "MaPhongBan", nhanVien.MaPhongBan);
            return View(nhanVien);
        }

        // GET: NhanViens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhanVien = await _context.NhanViens
                .Include(n => n.MaPhongBanNavigation)
                .FirstOrDefaultAsync(m => m.MaNhanVien == id);
            if (nhanVien == null)
            {
                return NotFound();
            }

            return View(nhanVien);
        }

        // POST: NhanViens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhanVien = await _context.NhanViens.FindAsync(id);
            if (nhanVien != null)
            {
                _context.NhanViens.Remove(nhanVien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanVienExists(int id)
        {
            return _context.NhanViens.Any(e => e.MaNhanVien == id);
        }
    }
}
