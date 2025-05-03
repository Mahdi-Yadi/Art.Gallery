import { Link } from "react-router";
import ThemeManager from "../ThemeManager";

const Header = () => {
  return (
    <header className="navbar-light navbar-sticky header-static">
      <nav className="navbar navbar-expand-lg">
        <div className="container">
          <Link to={"/"} className="navbar-brand">
            <img
              className="navbar-brand-item light-mode-item"
              src="/assets/images/logo.svg"
              alt="logo"
            />
            <img
              className="navbar-brand-item dark-mode-item"
              src="/assets/images/logo-light.svg"
              alt="logo"
            />
          </Link>

          <button
            className="navbar-toggler ms-auto"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarCollapse"
            aria-controls="navbarCollapse"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="text-body h6 d-none d-sm-inline-block">منو</span>
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse justify-content-center" id="navbarCollapse">
            <ul className="navbar-nav navbar-nav-scrol">
              <li className="nav-item dropdown">
                <a
                  className="nav-link dropdown-toggle active"
                  href="#"
                  id="homeMenu"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  خانه
                </a>
                <ul className="dropdown-menu" aria-labelledby="homeMenu">
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index.html">
                      نسخه 1
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-lazy.html">
                      نسخه 2
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item active" href="index-3.html">
                      نسخه 3
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-4.html">
                      نسخه 4
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-5.html">
                      نسخه 5
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-6.html">
                      نسخه 6
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-7.html">
                      نسخه 7
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-8.html">
                      نسخه 8
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-9.html">
                      نسخه 9
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-10.html">
                      نسخه 10
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-11.html">
                      نسخه 11
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="index-12.html">
                      نسخه 12
                    </a>
                  </li>
                </ul>
              </li>

              <li className="nav-item dropdown">
                <a
                  className="nav-link dropdown-toggle"
                  href="#"
                  id="pagesMenu"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  صفحات
                </a>
                <ul className="dropdown-menu" aria-labelledby="pagesMenu">
                  <li>
                    {" "}
                    <a className="dropdown-item" href="about-us.html">
                      درباره ما
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="contact-us.html">
                      تماس با ما
                    </a>
                  </li>

                  <li className="dropdown-submenu dropend">
                    <a className="dropdown-item dropdown-toggle" href="#">
                      فروشگاه{" "}
                      <span className="badge bg-danger smaller me-1">جدید</span>
                    </a>
                    <ul
                      className="dropdown-menu dropdown-menu-start"
                      data-bs-popper="none"
                    >
                      <li>
                        {" "}
                        <a className="dropdown-item" href="shop-grid.html">
                          لیست محصول{" "}
                          <span className="badge bg-danger smaller me-1">جدید</span>
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="shop-detail.html">
                          جزئیات محصول
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="checkout.html">
                          تسویه حساب
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="my-cart.html">
                          سبد خرید
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="empty-cart.html">
                          سبد خرید خالی
                        </a>{" "}
                      </li>
                    </ul>
                  </li>

                  <li className="dropdown-submenu dropend">
                    <a className="dropdown-item dropdown-toggle" href="#">
                      صفحات
                    </a>
                    <ul
                      className="dropdown-menu dropdown-menu-start"
                      data-bs-popper="none"
                    >
                      <li>
                        {" "}
                        <a className="dropdown-item" href="author.html">
                          نویسنده
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="categories.html">
                          دسته بندی نسخه 1
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="categories-2.html">
                          دسته بندی نسخه 2
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="tag.html">
                          # برچسب
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="search-result.html">
                          نتیجه جستجو
                        </a>{" "}
                      </li>
                    </ul>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="404.html">
                      صفحه 404
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="signin.html">
                      ورود
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="signup.html">
                      ثبت نام
                    </a>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="offline.html">
                      غیرفعال
                    </a>
                  </li>

                  <li className="dropdown-divider"></li>
                  <li className="dropdown-submenu dropend">
                    <a className="dropdown-item dropdown-toggle" href="#">
                      زیر منو
                    </a>
                    <ul
                      className="dropdown-menu dropdown-menu-start"
                      data-bs-popper="none"
                    >
                      <li className="dropdown-submenu dropend">
                        <a className="dropdown-item dropdown-toggle" href="#">
                          نسخه 1
                        </a>
                        <ul className="dropdown-menu" data-bs-popper="none">
                          <li>
                            {" "}
                            <a className="dropdown-item" href="#">
                              عنوان 1
                            </a>{" "}
                          </li>
                          <li>
                            {" "}
                            <a className="dropdown-item" href="#">
                              عنوان 2
                            </a>{" "}
                          </li>
                        </ul>
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="#">
                          {" "}
                          نسخه 2
                        </a>{" "}
                      </li>

                      <li className="dropdown-submenu dropstart">
                        <a className="dropdown-item dropdown-toggle" href="#">
                          نسخه 3
                        </a>
                        <ul
                          className="dropdown-menu dropdown-menu-end"
                          data-bs-popper="none"
                        >
                          <li>
                            {" "}
                            <a className="dropdown-item" href="#">
                              عنوان 1
                            </a>{" "}
                          </li>
                          <li>
                            {" "}
                            <a className="dropdown-item" href="#">
                              عنوان 2
                            </a>{" "}
                          </li>
                        </ul>
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="#">
                          نسخه 4
                        </a>{" "}
                      </li>
                    </ul>
                  </li>
                  <li className="dropdown-divider"></li>
                  <li>
                    <a className="dropdown-item" href="#" target="_blank">
                      <i className="text-warning fa-fw bi bi-life-preserver me-2"></i>
                      پشتیبانی
                    </a>
                  </li>
                  <li>
                    <a
                      className="dropdown-item"
                      href="../rtl/docs/index.html"
                      target="_blank"
                    >
                      <i className="text-danger fa-fw bi bi-card-text me-2"></i>
                      داکیومنت
                    </a>
                  </li>
                  {/* <li className="dropdown-divider"></li>
                  <li>
                    <a
                      className="dropdown-item"
                      href="../ltr/index.html"
                      target="_blank"
                    >
                      <i className="text-info fa-fw bi bi-toggle-off me-2"></i>نسخه
                      LTR
                    </a>
                  </li>
                  <li>
                    <a className="dropdown-item" href="#" target="_blank">
                      <i className="text-success fa-fw bi bi-cloud-download-fill me-2"></i>
                      خرید قالب
                    </a>
                  </li> */}
                </ul>
              </li>

              <li className="nav-item dropdown">
                <a
                  className="nav-link dropdown-toggle"
                  href="#"
                  id="postMenu"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  لیست مقالات
                </a>
                <ul className="dropdown-menu" aria-labelledby="postMenu">
                  <li className="dropdown-submenu dropend">
                    <a className="dropdown-item dropdown-toggle" href="#">
                      شبکه ای
                    </a>
                    <ul
                      className="dropdown-menu dropdown-menu-start"
                      data-bs-popper="none"
                    >
                      <li>
                        {" "}
                        <a className="dropdown-item" href="post-grid.html">
                          نسخه 1
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="post-grid-4-col.html">
                          نسخه 2
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a className="dropdown-item" href="post-grid-masonry.html">
                          نسخه 3
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a
                          className="dropdown-item"
                          href="post-grid-masonry-filter.html"
                        >
                          نسخه 4
                        </a>{" "}
                      </li>
                      <li>
                        {" "}
                        <a
                          className="dropdown-item"
                          href="post-large-and-grid.html"
                        >
                          نسخه 5
                        </a>{" "}
                      </li>
                    </ul>
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-list.html">
                      لیست نسخه 1
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-list-2.html">
                      لیست نسخه 2
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-cards.html">
                      لیست نسخه 3
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-overlay.html">
                      لیست نسخه 4
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-types.html">
                      لیست نسخه 5
                    </a>{" "}
                  </li>
                  <li className="dropdown-divider"></li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-single.html">
                      جزئیات نسخه 1
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-single-2.html">
                      جزئیات نسخه 2
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-single-3.html">
                      جزئیات نسخه 3
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-single-4.html">
                      جزئیات نسخه 4
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-single-5.html">
                      جزئیات نسخه 5
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="post-single-6.html">
                      جزئیات نسخه 6
                    </a>{" "}
                  </li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="podcast-single.html">
                      جزئیات نسخه 7
                    </a>{" "}
                  </li>
                  <li className="dropdown-divider"></li>
                  <li>
                    {" "}
                    <a className="dropdown-item" href="pagination-styles.html">
                      سبک های صفحه بندی
                    </a>{" "}
                  </li>
                </ul>
              </li>

              <li className="nav-item dropdown dropdown-fullwidth">
                <a
                  className="nav-link dropdown-toggle"
                  href="#"
                  id="megaMenu"
                  data-bs-toggle="dropdown"
                  aria-haspopup="true"
                  aria-expanded="false"
                >
                  {" "}
                  مگامنو
                </a>
                <div className="dropdown-menu" aria-labelledby="megaMenu">
                  <div className="container">
                    <div className="row g-4 p-3 flex-fill">
                      <div className="col-sm-6 col-lg-3">
                        <div className="card bg-transparent">
                          <img
                            className="card-img rounded"
                            src="assets/images/blog/16by9/small/01.jpg"
                            alt="Card image"
                          />
                          <div className="card-body px-0 pt-3">
                            <h6 className="card-title mb-0">
                              <a href="#" className="btn-link text-reset">
                                7 دیدگاه اشتباهاتی که همه در سفر مرتکب می شوند؟
                              </a>
                            </h6>

                            <ul className="nav nav-divider align-items-center text-uppercase small mt-2">
                              <li className="nav-item">
                                <a href="#" className="text-reset btn-link">
                                  الناز حسینی
                                </a>
                              </li>
                              <li className="nav-item">15 بهمن، 1400</li>
                            </ul>
                          </div>
                        </div>
                      </div>

                      <div className="col-sm-6 col-lg-3">
                        <div className="card bg-transparent">
                          <img
                            className="card-img rounded"
                            src="assets/images/blog/16by9/small/02.jpg"
                            alt="Card image"
                          />
                          <div className="card-body px-0 pt-3">
                            <h6 className="card-title mb-0">
                              <a href="#" className="btn-link text-reset">
                                12 بدترین نوع حساب های تجاری که در توییتر دنبال
                                می کنید!
                              </a>
                            </h6>

                            <ul className="nav nav-divider align-items-center text-uppercase small mt-2">
                              <li className="nav-item">
                                <a href="#" className="text-reset btn-link">
                                  محمد کریمی
                                </a>
                              </li>
                              <li className="nav-item">2 آبان، 1400</li>
                            </ul>
                          </div>
                        </div>
                      </div>

                      <div className="col-sm-6 col-lg-3">
                        <div className="card bg-transparent">
                          <img
                            className="card-img rounded"
                            src="assets/images/blog/16by9/small/03.jpg"
                            alt="Card image"
                          />
                          <div className="card-body px-0 pt-3">
                            <h6 className="card-title mb-0">
                              <a href="#" className="btn-link text-reset">
                                آیا این آگهی ها واقعی هستند؟ معاوضه لوازم شخصی
                                با غذا!
                              </a>
                            </h6>

                            <ul className="nav nav-divider align-items-center text-uppercase small mt-2">
                              <li className="nav-item">
                                <a href="#" className="text-reset btn-link">
                                  مهدی شجاعی
                                </a>
                              </li>
                              <li className="nav-item">14 مرداد، 1400</li>
                            </ul>
                          </div>
                        </div>
                      </div>

                      <div className="col-sm-6 col-lg-3">
                        <div className="bg-primary bg-opacity-10 p-4 text-center h-100 w-100 rounded">
                          <span>پیشنهاد Blogzine</span>
                          <h3>خرید پکیج اقتصادی</h3>
                          <p>عضویت در خبرنامه</p>
                          <a href="#" className="btn btn-warning">
                            خرید و فعالسازی
                          </a>
                        </div>
                      </div>
                    </div>

                    <div className="row px-3">
                      <div className="col-12">
                        <ul className="list-inline mt-3">
                          <li className="list-inline-item">برچسب ها:</li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-primary-soft">
                              گردشگری
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-warning-soft">
                              کسب و کار
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-success-soft">
                              فناوری
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-danger-soft">
                              گجت
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-info-soft">
                              سبک زندگی
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-primary-soft">
                              واکسن
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-success-soft">
                              ورزشی
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-danger-soft">
                              کووید
                            </a>
                          </li>
                          <li className="list-inline-item">
                            <a href="#" className="btn btn-sm btn-info-soft">
                              پوشاک
                            </a>
                          </li>
                        </ul>
                      </div>
                    </div>
                  </div>
                </div>
              </li>

              <li className="nav-item">
                {" "}
                <a className="nav-link" href="dashboard.html">
                  داشبورد
                </a>
              </li>
            </ul>
          </div>

          <div className="nav flex-nowrap align-items-center ms-3 ms-sm-4">
            
            <ThemeManager/>

            <div className="nav-item dropdown dropdown-toggle-icon-none d-none d-sm-block">
              <a
                className="nav-link dropdown-toggle"
                role="button"
                href="#"
                id="navAdditionalLink"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <i className="bi bi-three-dots fs-4"></i>
              </a>
              <ul
                className="dropdown-menu dropdown-menu-start min-w-auto shadow rounded text-start"
                aria-labelledby="navAdditionalLink"
              >
                <li>
                  <a className="dropdown-item fw-normal" href="#">
                    درباره ما
                  </a>
                </li>
                <li>
                  <a className="dropdown-item fw-normal" href="#">
                    خبرنامه
                  </a>
                </li>
                <li>
                  <a className="dropdown-item fw-normal" href="#">
                    نویسنده
                  </a>
                </li>
                <li>
                  <a className="dropdown-item fw-normal" href="#">
                    #برچسب
                  </a>
                </li>
                <li>
                  <a className="dropdown-item fw-normal" href="#">
                    تماس با ما
                  </a>
                </li>
                <li>
                  <a className="dropdown-item fw-normal" href="#">
                    <span className="badge bg-danger me-2 align-middle">2 </span>
                    اقتصاد
                  </a>
                </li>
              </ul>
            </div>

            <div className="nav-item d-none d-md-block">
              <Link to={"/Register"} className="btn btn-sm btn-danger mb-0 mx-2">
                عضویت
              </Link>
            </div>

            <div className="nav-item dropdown nav-search dropdown-toggle-icon-none">
              <a
                className="nav-link pe-0 dropdown-toggle"
                role="button"
                href="#"
                id="navSearch"
                data-bs-toggle="dropdown"
                aria-expanded="false"
              >
                <i className="bi bi-search fs-4"> </i>
              </a>
              <div
                className="dropdown-menu dropdown-menu-end shadow rounded p-2"
                aria-labelledby="navSearch"
              >
                <form className="input-group">
                  <input
                    className="form-control border-success"
                    type="search"
                    placeholder="جستجو"
                    aria-label="Search"
                  />
                  <button className="btn btn-success m-0" type="submit">
                    جستجو
                  </button>
                </form>
              </div>
            </div>
          </div>
        </div>
      </nav>
    </header>
  );
};

export default Header;
