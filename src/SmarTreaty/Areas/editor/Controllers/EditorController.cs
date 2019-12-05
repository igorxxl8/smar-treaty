using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmarTreaty.Areas.editor.Controllers
{
    [RouteArea("editor")]
    [Authorize(Roles="editor")]
    public abstract class EditorController : Controller
    {
    }
}