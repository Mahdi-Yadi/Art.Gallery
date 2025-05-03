const ProductsCardItem = (Props) => {
  function CheckPrice() {
    if (Props.OldPrice) {
      return (
        <div className="d-flex align-items-center justify-content-center">
          <h6 className="text-success mb-0 me-2">{Props.Price}</h6>
          <small className="text-decoration-line-through">{Props.OldPrice}</small>
        </div>
      );
    } else {
      return <h6 className="text-success mb-0 me-2">{Props.Price}</h6>;
    }
  }

  function CheckDiscount() {
    if (Props.DiscountAmount) {
      return (
        <div className="card-img-overlay p-0">
          <span className="badge text-bg-danger">{Props.DiscountAmount + "% تخفیف"}</span>
        </div>
      );
    }
  }

  return (
    <div>
      <div className="card border p-3 h-100">
        <div className="position-relative">
          <a href="shop-detail.html" className="position-relative">
            <img className="card-img" src={Props.CardImage} alt="" />
          </a>
          {CheckDiscount()}
        </div>

        <div className="card-body text-center p-3 px-0">
          <div className="d-flex justify-content-center mb-2">
            <ul className="list-inline mb-0">
              <li className="list-inline-item me-0 small">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0 small">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0 small">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0 small">
                <i className="fas fa-star text-warning"></i>
              </li>
              <li className="list-inline-item me-0 small">
                <i className="fas fa-star-half-alt text-warning"></i>
              </li>
            </ul>
          </div>

          <h5 className="card-title">
            <a href="shop-detail.html">{Props.Title}</a>
          </h5>

          {CheckPrice()}
        </div>

        <div className="card-footer text-center p-0">
          <a href="#" className="btn btn-sm btn-primary-soft mb-0">
            <i className="bi bi-cart me-2"></i>
            افزودن به سبد
          </a>
        </div>
      </div>
    </div>
  );
};

export default ProductsCardItem;
