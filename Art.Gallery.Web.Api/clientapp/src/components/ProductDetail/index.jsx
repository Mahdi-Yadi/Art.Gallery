import ProductGallery from "../ProductGallery";

const ProductDetail = () => {
  return (
    <section>
      <div className="container">
        <div className="row g-4 g-lg-0 justify-content-between">
          {/* -----| Images |----- */}
          <div className="col-lg-5">
            <ProductGallery />
          </div>
          {/* -----| End of Images |----- */}

          {/* -----| Detail |----- */}
          <div className="col-lg-6">
            {/* --- Title --- */}
            <h1>هواپیما اسباب بازی کنترلی</h1>
            <p className="mb-4">
              چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است
              و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف
              بهبود ابزارهای کاربردی می باشد.
            </p>
            {/* --- End of Title --- */}

            {/* --- Rating Section --- */}
            <ul className="list-inline mb-0">
              <li className="list-inline-item me-0">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0">
                <i className="fas fa-star-half-alt text-warning"></i>
              </li>
              <li className="list-inline-item me-0 h6">4.5/5.0</li>
            </ul>
            {/* --- End of Rating Section --- */}

            {/* --- Product Detail --- */}
            <div className="bg-light p-3 rounded-2 mb-4">
              <div className="row g-2">
                <div className="col-sm-6">
                  <ul className="list-group list-group-borderless">
                    <li className="list-group-item py-0">
                      <span>وضعیت:</span>
                      <span className="h4 mb-0">جدید</span>
                    </li>
                    <li className="list-group-item pb-0">
                      <span>دسته بندی:</span>
                      <span className="h4 mb-0">
                        <a href="shop-grid.html">لوازم دیجیتال</a>
                      </span>
                    </li>
                  </ul>
                </div>
                <div className="col-sm-6">
                  <ul className="list-group list-group-borderless">
                    <li className="list-group-item py-0">
                      <span>موجود در انبار:</span>
                      <span className="h4 mb-0">15</span>
                    </li>
                    <li className="list-group-item pb-0">
                      <span>وزن:</span>
                      <span className="h4 mb-0">2.2Kg</span>
                    </li>
                  </ul>
                </div>
                <div className="col-sm-6">
                  <ul className="list-group list-group-borderless">
                    <li className="list-group-item py-0">
                      <span>ابعاد:</span>
                      <span className="h4 mb-0">10 * 15 * 7 سانتی متر</span>
                    </li>
                  </ul>
                </div>
              </div>
            </div>
            {/* --- End of Product Detail --- */}

            {/* --- Add To Cart --- */}
            <div className="row align-items-center">
              <div className="col-md-4">
                <h6>جمع کل:</h6>
                <h4 className="text-success h1 mb-0">598000 تومان</h4>
              </div>

              <div className="col-md-2 pe-md-0">
                <select
                  className="form-select"
                  aria-label="Default select example"
                >
                  <option value="1">01</option>
                  <option value="2">02</option>
                  <option value="3">03</option>
                </select>
              </div>

              <div className="col-md-6">
                <a href="#" className="btn btn-primary mb-0 w-100">
                  <i className="bi bi-cart2 me-2"></i>
                  افزودن به سبد
                </a>
              </div>
            </div>
            {/* --- End of Add To Cart --- */}
          </div>
          {/* -----| end of Detail |----- */}
        </div>
      </div>
    </section>
  );
};

export default ProductDetail;
