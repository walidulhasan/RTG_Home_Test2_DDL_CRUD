using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AddressBook.Data;
using AddressBook.Models;
using Microsoft.AspNetCore.Hosting;
using AddressBook.Models.View_Model;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Data;

namespace AddressBook.Controllers
{
    public class PeopleController : Controller
    {
        private readonly AddressBookDbContext _context;
        private readonly IWebHostEnvironment _he;
        //database_access_layer.db dbop = new database_access_layer.db();

        public PeopleController(AddressBookDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: People
        public async Task<IActionResult> Index()
        {
            var addressBookDbContext = _context.persons
                                        .Include(p => p.country)
                                        .Include(p => p.district)
                                        .Include(p => p.division);
            
            return View(await addressBookDbContext.ToListAsync());
            
        }
        //Searching
       

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _context.persons
                                .Include(p => p.country)
                                .Include(p => p.district)
                                .Include(p => p.division)
                                .FirstOrDefaultAsync(m => m.personId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        #region For Ajax Form

        public IActionResult ajaxDataSave()
        {
            ViewBag.Countries = new SelectList(_context.countries, "countryId", "countryName");
            return View();
        }

        #endregion

        // GET: People/Create
        public IActionResult Create()
        {

            ViewBag.Countries = new SelectList(_context.countries, "countryId", "countryName");

            //var CuntList = _context.countries.OrderBy(x => x.countryName);
            //ViewBag.Country = CuntList;



            //ViewData["countryId"] = new SelectList(_context.countries, "countryId", "countryName");
            //ViewData["districtId"] = new SelectList(_context.districts, "districtId", "districtName");
            //ViewData["divisionId"] = new SelectList(_context.divisions, "divisionId", "divisionName");


            //DataSet ds = dbop.GetCountry();
            //List<SelectListItem> list = new List<SelectListItem>();
            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    list.Add(new SelectListItem { Text = dr["countryName"].ToString(), Value = dr["countryId"].ToString() });
            //}
            //ViewBag.CountryList = list;


            return View();
        }
        public JsonResult getDivision(int id)
        {
            var division = _context.divisions.Where(x => x.CountryId == id).ToList();
            return Json(new SelectList(division,"divisionId", "divisionName"));

            //var DisList = _context.divisions.Where(d => d.divisionId == id).OrderBy(d => d.divisionName);
            //return Json(DisList.ToList());
        }
        //public JsonResult getDivision(int id)
        //{
        //    //var DivList = _context.divisions.Where(d => d.divisionId == id).OrderBy(d => d.divisionName);
        //    //return Json(DivList.ToList());
        //    DataSet ds = dbop.GetDivision(id);
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        list.Add(new SelectListItem { Text = dr["districtName"].ToString(), Value = dr["divisionId"].ToString() });
        //    }
        //    return Json(list);
        //}
        //public JsonResult getDistrict(int id)
        //{
        //    DataSet ds = dbop.GetDistrict(id);
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    foreach (DataRow dr in ds.Tables[0].Rows)
        //    {
        //        list.Add(new SelectListItem { Text = dr["divisionName"].ToString(), Value = dr["districtId"].ToString() });
        //    }
        //    return Json(list);
        //}
        public JsonResult getDistrict(int id)
        {
            var district = _context.districts.Where(x => x.DivisionId == id).ToList();
            return Json(new SelectList(district, "districtId", "districtName"));
            //var DisList = _context.districts.Where(d => d.districtId == id).OrderBy(d => d.districtName);
            //return Json(DisList.ToList());
        }
        // POST: People/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("personId,personName,personPhone,PicturPath,dob,village,countryId,divisionId,districtId")] PersonVM personVM)
        {
            string msg = "";
            if (ModelState.IsValid)
            {
                var foundPhoneNumber = await _context.persons.FirstOrDefaultAsync(x => x.personPhone.Equals(personVM.personPhone));

                if (foundPhoneNumber != null)
                {

                    //Response.Redirect("/People/Create");
                    ViewBag.msg = "Phone number already exists";
                }
                else if(foundPhoneNumber == null)
                {
                    string imageFileName = null;

                    Person c = new Person();
                    c.personName = personVM.personName;

                    c.personPhone = personVM.personPhone;
                    c.dob = personVM.dob;
                    c.village = personVM.village;
                    c.countryId = personVM.countryId;
                    c.divisionId = personVM.divisionId;
                    c.districtId = personVM.districtId;

                    if (personVM.PicturPath != null)
                    {
                        //Image
                        string webroot = _he.WebRootPath;
                        string folder = "Images";
                        imageFileName = Guid.NewGuid() + "_" + Path.GetFileName(personVM.PicturPath.FileName);
                        string fileToWrite = Path.Combine(webroot, folder, imageFileName);
                        using (var stream = new FileStream(fileToWrite, FileMode.Create))
                        {
                            await personVM.PicturPath.CopyToAsync(stream);
                        }
                    }

                    c.personPicture = imageFileName;
                    c.countryId = personVM.countryId;
                    _context.Add(c);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                } 
            }
            else
            {
                msg = "Person data is incomplete. Please try again.";
            }

            ViewData["countryId"] = new SelectList(_context.countries, "countryId", "countryName", personVM.countryId);
            ViewData["districtId"] = new SelectList(_context.districts, "districtId", "districtName", personVM.districtId);
            ViewData["divisionId"] = new SelectList(_context.divisions, "divisionId", "divisionName", personVM.divisionId);

            return View(personVM);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var person = await _context.persons.FindAsync(id);
            //if (person == null)
            //{
            //    return NotFound();
            //}
            if (Id == null)
            {
                return NotFound();
            }

            var person = await _context.persons.FindAsync(Id);
            if (person == null)
            {
                return NotFound();
            }
            PersonVM evm = new PersonVM
            {
                personId=person.personId,
                personName = person.personName,
                personPhone = person.personPhone,
                dob = person.dob,
                village = person.village,
                personPicture = person.personPicture,
                countryId = person.countryId,
                divisionId = person.divisionId,
                districtId = person.districtId
            };
            ViewData["countryId"] = new SelectList(_context.countries, "countryId", "countryName", person.countryId);
            ViewData["districtId"] = new SelectList(_context.districts, "districtId", "districtName", person.districtId);
            ViewData["divisionId"] = new SelectList(_context.divisions, "divisionId", "divisionName", person.divisionId);
            return View(evm);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("personId,personName,personPhone,PicturPath,dob,village,countryId,divisionId,districtId")] PersonVM personVM)
        {
           
            if (id != personVM.personId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                var persons = await _context.persons.FindAsync(id);
                string imageFileName = persons.personPicture;
                try
                {
                    if (personVM.PicturPath != null)
                    {


                        //var path2 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", persons.personPicture);
                        //System.IO.File.Delete(path2);
                       
                        
                        if (personVM.PicturPath!=null)
                        {
                            string webroot = _he.WebRootPath;
                            string folder = "Images";
                            imageFileName = Guid.NewGuid() + "_" + Path.GetFileName(personVM.PicturPath.FileName);
                            string fileToWrite = Path.Combine(webroot, folder, imageFileName);
                            using (var stream = new FileStream(fileToWrite, FileMode.Create))
                            {
                                await personVM.PicturPath.CopyToAsync(stream);
                            }
                        }
                        

                        persons.personId = personVM.personId;
                        persons.personName = personVM.personName;
                        persons.dob = personVM.dob;
                        persons.village = personVM.village;
                        persons.personPicture = imageFileName;
                        persons.countryId = personVM.countryId;
                        persons.divisionId = personVM.divisionId;
                        persons.districtId = personVM.districtId;


                       
                        _context.Update(persons);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {

                        persons.personId = personVM.personId;
                        persons.personName = personVM.personName;
                        persons.dob = personVM.dob;
                        persons.village = personVM.village;
                        persons.personPicture = imageFileName;
                        persons.countryId = personVM.countryId;
                        persons.divisionId = personVM.divisionId;
                        persons.districtId = personVM.districtId;
                        _context.Update(persons);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(personVM.personId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewData["countryId"] = new SelectList(_context.countries, "countryId", "countryName", personVM.countryId);
            ViewData["districtId"] = new SelectList(_context.districts, "districtId", "districtName", personVM.districtId);
            ViewData["divisionId"] = new SelectList(_context.divisions, "divisionId", "divisionName", personVM.divisionId);
            return View(personVM);
        }

        // GET: People/Delete/5
        public IActionResult Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var person = await _context.persons
            //    .Include(p => p.country)
            //    .Include(p => p.district)
            //    .Include(p => p.division)
            //    .FirstOrDefaultAsync(m => m.personId == id);
            //if (person == null)
            //{
            //    return NotFound();
            //}

            //return View(person);

            Person person = _context.persons.Find(id);
            if (person.personPicture != null)
            {
                //For Image Delete Form Folder
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images", person.personPicture);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                //Delete Data
                var del = (from Client in _context.persons where Client.personId == id select Client).FirstOrDefault();
                _context.persons.Remove(del);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                //Delete Data
                var del = (from Client in _context.persons where Client.personId == id select Client).FirstOrDefault();
                _context.persons.Remove(del);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
           
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _context.persons.FindAsync(id);
            _context.persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            return _context.persons.Any(e => e.personId == id);
        }
    }
}
