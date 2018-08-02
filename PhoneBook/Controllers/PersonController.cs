using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PhoneBook.Controllers
{
	public class PersonController : Controller
	{
		public IActionResult Index(int page = 1, string search = null)
		{
			//TODO Stworzyć formularz wyszukiwarkę osoby i przerzucić wyświetlenie osób na osobny widok
			int pagemax=0;
			do
			{
				pagemax++;
			}
			while (SourceManager.Get(pagemax, 4).Any());

			int pagemaxsearch = 0;
			do
			{
				pagemaxsearch++;
			}
			while (SourceManager.GetByName(search, pagemax, 4).Any());

			ViewBag.Search = search;
			ViewBag.pagemax = pagemax;
			ViewBag.pagemaxsearch = pagemaxsearch;
			ViewBag.page = page;


			if (search != null)
			{
				return View(SourceManager.GetByName(search, page, 4));
			}
					
			else
			{
				return View(SourceManager.Get(page, 4));
			}
			
		}
	   

		[HttpGet]
		public IActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Add(PersonModel personModel)
		{
			if (ModelState.IsValid)	   //walidacja
			{ 
				var id = SourceManager.Add(personModel);

				return Redirect("Index");
			}
			return View(personModel);
		}




		[HttpGet]
		public IActionResult Edit(int id)
		{
			PersonModel personModel = SourceManager.GetByID(id);
			if (personModel == null)
			{
				return StatusCode(404);
			}
			return View(personModel);
		}

		[HttpPost]
		public IActionResult Edit(PersonModel personModel)
		{
			if (ModelState.IsValid)    //walidacja
			{
				SourceManager.Update(personModel);
				return Redirect("Index");
			}
			return View(personModel);
		  
		}

		[HttpGet]
		public IActionResult Remove(int id)
		{
			PersonModel personModel = SourceManager.GetByID(id);
			if (personModel == null)
			{
				return StatusCode(404);
			}
			return View(personModel);
		}

		[HttpPost]
		public IActionResult Remove(PersonModel personModel)
		{
			SourceManager.Remove(personModel);
			return Redirect("Index");
		}

	}

	
	//TODO slajd 11 zmiana routingu

}
