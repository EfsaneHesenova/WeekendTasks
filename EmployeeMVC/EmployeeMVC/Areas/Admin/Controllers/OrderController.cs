using EmployeeMVC.Areas.Admin.ViewModels;
using EmployeeMVC.DAL;
using EmployeeMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Manager")]
    public class OrderController : Controller
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Order> orders = await _context.Orders.ToListAsync();
            return View(orders);
        }

        public IActionResult Delete(int id)
        {
            Order? order = _context.Orders.Find(id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            OrderVM model = new OrderVM()
            {
                Services = _context.Services.Where(s => s.IsActive && !s.IsDeleted).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                }),
                Masters = _context.Masters.Where(s => !s.IsDeleted && s.IsActive).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(Order order)
        {
            if (!ModelState.IsValid)
            {
                OrderVM model = new OrderVM()
                {
                    Services = (List<SelectListItem>)_context.Services.Where(s => s.IsActive && !s.IsDeleted).Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Title
                    }),
                    Masters = (List<SelectListItem>)_context.Masters.Where(s => !s.IsDeleted).Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name
                    }),
                    Order = order
                };
                ModelState.AddModelError(string.Empty, "Something wrong");
                return View(model);
            }

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            Order? app = _context.Orders.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            OrderVM model = new OrderVM()
            {
                Order = app,
                Masters = _context.Masters.Where(d => d.IsActive && !d.IsDeleted && d.ServiceId == app.ServiceId)
                                          .Select(d => new SelectListItem
                                          {
                                              Value = d.Id.ToString(),
                                              Text = d.Name + " " + d.SurName
                                          }).ToList(),
                Services = _context.Services.Where(p => !p.IsDeleted)
                                            .Select(p => new SelectListItem
                                            {
                                                Value = p.Id.ToString(),
                                                Text = p.Title
                                            }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(int id, Order order)
        {
            Order? app = _context.Orders.Find(id);
            if (app == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                OrderVM model = new OrderVM()
                {
                    Order = app,
                    Masters = _context.Masters.Where(d => d.IsActive && !d.IsDeleted && d.ServiceId == app.ServiceId)
                                              .Select(d => new SelectListItem
                                              {
                                                  Value = d.Id.ToString(),
                                                  Text = d.Name + " " + d.SurName
                                              }).ToList(),
                    Services = _context.Services.Where(p => !p.IsDeleted)
                                                .Select(p => new SelectListItem
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Title
                                                }).ToList()
                };
                return View(model);
            }

            app.ClientName = order.ClientName;
            app.ClientSurname = order.ClientSurname;
            app.ClientPhoneNumber = order.ClientPhoneNumber;
            app.ClientEmail = order.ClientEmail;
            app.ServiceId = order.ServiceId;
            app.MasterId = order.MasterId;
            app.IsActive = order.IsActive;
            app.Problem = order.Problem;
            app.ServiceId = order.ServiceId;

            _context.Orders.Update(app);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}


