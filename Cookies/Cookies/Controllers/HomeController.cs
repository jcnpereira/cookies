using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cookies.Controllers {
	public class HomeController:Controller {

		// GET: Home
		public ActionResult Index() {

			// recuperar o 'cookie'
			HttpCookie aCookie = HttpContext.Request.Cookies.Get("ExemploCookies");

			if(aCookie != null) {
				// se existe 'cookie', recuperam-se os dados associados ao 'cookie'
				// recuperar o nome do "tipo de transporte"
				ViewBag.Transporte = aCookie.Values["Transporte"];
				// e a data da última visita
				ViewBag.UltimaVisita = aCookie.Values["UltimaVisita"];
			}

			return View();
		}


		/// <summary>
		/// Adição de cookies
		/// </summary>
		/// <param name="tipoTransporte">tipo de transporte utilizado</param>
		/// <returns></returns>
		[HttpPost]
		public ActionResult AddCookie(string tipoTransporte) {
			// https://msdn.microsoft.com/en-us/library/ms178194.aspx
			//	http://www.programmerguide.net/2014/12/creating-cookie-in-aspnet-mvc-action.html
			//	http://stackoverflow.com/questions/6797350/asp-net-mvc-cookie-implementation

			try {
				if(!string.IsNullOrEmpty(tipoTransporte)) {
					// se foi escolhido um meio de transporte...
					// criar o 'cookie'
					HttpCookie aCookie = new HttpCookie("ExemploCookies");
					aCookie.Values["Transporte"] = tipoTransporte;
					aCookie.Values["UltimaVisita"] = DateTime.Now.ToString();
					aCookie.Expires = DateTime.Now.AddDays(30);
					//	escreve o cookie no disco
					Response.Cookies.Add(aCookie);
				}
			}
			catch(Exception) {
				ModelState.AddModelError("","Ocorreu um erro na gravação do Cookie...");
			}

			return RedirectToAction("Index");
		}


		/// <summary>
		/// Remoção de cookies do sistema
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public ActionResult DeleteCookie() {

			// recuperar o 'cookie'
			HttpCookie cookie = HttpContext.Request.Cookies.Get("ExemploCookies");

			if(cookie != null) {
				// o 'cookie' existe
				// anula o cookie, atribuindo-lhe uma data de expiração já caducada
				cookie.Expires = DateTime.Now.AddDays(-2);
				HttpContext.Response.SetCookie(cookie);
			}

			return RedirectToAction("Index");

		}
	}
}