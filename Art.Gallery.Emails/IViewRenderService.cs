namespace Art.Gallery.Emails;
public interface IViewRenderService
{
   string RenderToString(string viewName, object model);
}