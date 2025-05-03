const Footer = () => {
  return (
    <footer className="pb-0">
      <div className="container">
        <hr />

        <div className="row pt-5">
          <div className="col-md-6 col-lg-4 mb-4">
            <img
              className="light-mode-item"
              src="/assets/images/logo.svg"
              alt="logo"
            />
            <img
              className="dark-mode-item"
              src="/assets/images/logo-light.svg"
              alt="logo"
            />
            <p className="mt-3">
              این قالب توسط Bootstrap5 طراحی شده.
            </p>
            <div className="mt-4">
              ©2025 طراحی شده توسط{" "}
              <a
                href="#"
                className="text-reset btn-link"
              >
                محمدمهدی امام هادی
              </a>
            </div>
          </div>

          <div className="col-md-6 col-lg-3 mb-4">
            <h5 className="mb-4">لینک های مفید</h5>
            <div className="row">
              <div className="col-6">
                <ul className="nav flex-column">
                  <li className="nav-item">
                    <a className="nav-link pt-0" href="#">
                      درباره ما
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      داشبورد
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      تماس با ما
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      خرید قالب
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      پشتیبانی
                    </a>
                  </li>
                </ul>
              </div>
              <div className="col-6">
                <ul className="nav flex-column">
                  <li className="nav-item">
                    <a className="nav-link pt-0" href="#">
                      اخبار
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      بین الملل <span className="badge text-bg-danger ms-2">2</span>
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      تکنولوژی
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      اقتصاد
                    </a>
                  </li>
                  <li className="nav-item">
                    <a className="nav-link" href="#">
                      سیاست
                    </a>
                  </li>
                </ul>
              </div>
            </div>
          </div>

          <div className="col-sm-6 col-lg-3 mb-4">
            <h5 className="mb-4">پربیننده ترین</h5>
            <ul className="list-inline">
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-primary-soft">
                  گردشگری
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-warning-soft">
                  اقتصاد
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-success-soft">
                  بورس
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-danger-soft">
                  قیمت طلا
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-info-soft">
                  فناوری و اطلاعات
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-primary-soft">
                  قیمت ارز امروز
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-warning-soft">
                  مگامنو
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-success-soft">
                  ورزش
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-danger-soft">
                  کووید
                </a>
              </li>
              <li className="list-inline-item">
                <a href="#" className="btn btn-sm btn-info-soft">
                  فرهنگ
                </a>
              </li>
            </ul>
          </div>

          <div className="col-sm-6 col-lg-2 mb-4">
            <h5 className="mb-4">شبکه های اجتماعی</h5>
            <ul className="nav flex-column">
              <li className="nav-item">
                <a className="nav-link pt-0" href="#">
                  <i className="fab fa-facebook-square fa-fw me-2 text-facebook"></i>
                  Facebook
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#">
                  <i className="fab fa-twitter-square fa-fw me-2 text-twitter"></i>
                  Twitter
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#">
                  <i className="fab fa-linkedin fa-fw me-2 text-linkedin"></i>
                  Linkedin
                </a>
              </li>
              <li className="nav-item">
                <a className="nav-link" href="#">
                  <i className="fab fa-youtube-square fa-fw me-2 text-youtube"></i>
                  YouTube
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </footer>
  );
};

export default Footer;
