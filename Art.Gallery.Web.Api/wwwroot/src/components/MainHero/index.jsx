import { Link } from "react-router";
import { Swiper, SwiperSlide } from "swiper/react";
import { Pagination, Keyboard } from "swiper/modules";
import Badge from "../common/Badge";

const MainHero = () => {
  return (
    <section className="p-0">
      <div className="container-fluid">
        <div className="row g-0">
          <div className="col-xxl-10 mx-auto rounded-3 overflow-hidden">
            <Swiper
              modules={[Pagination, Keyboard]}
              slidesPerView={1}
              keyboard={{
                enabled: true,
              }}
              loop={true}
              speed={400}
              pagination={{ clickable: true, dynamicBullets: true }}
            >
              {/* =========|Slides|========= */}
              <SwiperSlide>
                <div
                  className="card bg-dark-overlay-3 rounded-0 h-400 h-lg-500 h-xl-700 position-relative overflow-hidden"
                  style={{
                    backgroundColor: "yellow",
                    backgroundImage:
                      "url(src/assets/image/blog/16by9/big/02.jpg)",
                    backgroundPosition: "center left",
                    backgroundSize: "cover",
                  }}
                >
                  <div className="card-img-overlay rounded-0 d-flex align-items-center">
                    <div className="container px-3 my-auto">
                      <div className="row">
                        <div className="col-lg-7">
                          <Badge
                            Text={"مگامنو"}
                            className={"badge text-bg-danger mb-2"}
                            LinkUrl={"#"}
                          />
                          <h2 className="text-white display-6">
                            <a
                              href="post-single-4.html"
                              className="btn-link text-reset fw-normal"
                            >
                              کریسمس چه فرقی با سال نوی میلادی دارد؟
                            </a>
                          </h2>
                          <p className="text-white">
                            شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات
                            پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار
                            گیرد.
                          </p>
                          <ul className="nav nav-divider text-white-force align-items-center d-none d-sm-inline-block">
                            <li className="nav-item">
                              <div className="nav-link">
                                <div className="d-flex align-items-center text-white position-relative">
                                  <div className="avatar avatar-sm">
                                    <img
                                      className="avatar-img rounded-circle"
                                      src="src/assets/image/avatar/11.jpg"
                                      alt="avatar"
                                    />
                                  </div>
                                  <span className="ms-3">
                                    با{" "}
                                    <Link
                                      to={"/Artist-Profile/سهیلا صالحی"}
                                      className="stretched-link text-reset btn-link"
                                    >
                                      سهیلا صالحی
                                    </Link>
                                  </span>
                                </div>
                              </div>
                            </li>
                            <li className="nav-item">15 دی، 1400</li>
                            <li className="nav-item">5 دقیقه زمان مطالعه</li>
                          </ul>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </SwiperSlide>

              <SwiperSlide>
                <div
                  className="card bg-dark-overlay-3 rounded-0 h-400 h-lg-500 h-xl-700 position-relative overflow-hidden"
                  style={{
                    backgroundColor: "red",
                    backgroundImage:
                      "url(src/assets/image/blog/16by9/big/01.jpg)",
                    backgroundPosition: "center left",
                    backgroundSize: "cover",
                  }}
                >
                  <div className="card-img-overlay rounded-0 d-flex align-items-center">
                    <div className="container px-3 my-auto">
                      <div className="row">
                        <div className="col-lg-7">
                          <Badge
                            Text={"گردشگری"}
                            className={"badge text-bg-warning mb-2"}
                            LinkUrl={"#"}
                          />
                          <h2 className="text-white display-6">
                            <a
                              href="post-single-6.html"
                              className="btn-link text-reset fw-normal"
                            >
                              تداوم تنفس هوای ناسالم در تهران
                            </a>
                          </h2>
                          <p className="text-white">
                            شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات
                            پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار
                            گیرد.
                          </p>
                          <ul className="nav nav-divider text-white-force align-items-center d-none d-sm-inline-block">
                            <li className="nav-item">
                              <div className="nav-link">
                                <div className="d-flex align-items-center text-white position-relative">
                                  <div className="avatar avatar-sm">
                                    <img
                                      className="avatar-img rounded-circle"
                                      src="src/assets/image/avatar/11.jpg"
                                      alt="avatar"
                                    />
                                  </div>
                                  <span className="ms-3">
                                    با{" "}
                                    <Link
                                      to={"/Artist-Profile/سهیلا صالحی"}
                                      className="stretched-link text-reset btn-link"
                                    >
                                      سهیلا صالحی
                                    </Link>
                                  </span>
                                </div>
                              </div>
                            </li>
                            <li className="nav-item">18 تیر، 1400</li>
                            <li className="nav-item">2 دقیقه زمان مطالعه</li>
                          </ul>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </SwiperSlide>

              <SwiperSlide>
                <div
                  className="card bg-dark-overlay-3 rounded-0 h-400 h-lg-500 h-xl-700 position-relative overflow-hidden"
                  style={{
                    backgroundColor: "purple",
                    backgroundImage:
                      "url(src/assets/image/blog/16by9/big/03.jpg)",
                    backgroundPosition: "center left",
                    backgroundSize: "cover",
                  }}
                >
                  <div className="card-img-overlay rounded-0 d-flex align-items-center">
                    <div className="container w-100 my-auto">
                      <div className="row">
                        <div className="col-lg-7">
                          <Badge
                            Text={"کووید"}
                            className={"badge bg-primary mb-2"}
                            LinkUrl={"#"}
                          />
                          <h2 className="text-white display-6">
                            <a
                              href="post-single-4.html"
                              className="btn-link text-reset fw-normal"
                            >
                              مشکل اولیه استارت آپ ها و راه حل آنها
                            </a>
                          </h2>
                          <p className="text-white">
                            شامل حروفچینی دستاوردهای اصلی و جوابگوی سوالات
                            پیوسته اهل دنیای موجود طراحی اساسا مورد استفاده قرار
                            گیرد.
                          </p>
                          <ul className="nav nav-divider text-white-force align-items-center d-none d-sm-inline-block">
                            <li className="nav-item">
                              <div className="nav-link">
                                <div className="d-flex align-items-center text-white position-relative">
                                  <div className="avatar avatar-sm">
                                    <img
                                      className="avatar-img rounded-circle"
                                      src="src/assets/image/avatar/08.jpg"
                                      alt="avatar"
                                    />
                                  </div>
                                  <span className="ms-3">
                                    با{" "}
                                    <Link
                                      to={"/Artist-Profile/مرجان مرادی"}
                                      className="stretched-link text-reset btn-link"
                                    >
                                      مرجان مرادی
                                    </Link>
                                  </span>
                                </div>
                              </div>
                            </li>
                            <li className="nav-item">8 دی، 1400</li>
                            <li className="nav-item">10 دقیقه زمان مطالعه</li>
                          </ul>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </SwiperSlide>
              {/* =========|End Slides|========= */}
            </Swiper>
          </div>
        </div>
      </div>
    </section>
  );
};

export default MainHero;
