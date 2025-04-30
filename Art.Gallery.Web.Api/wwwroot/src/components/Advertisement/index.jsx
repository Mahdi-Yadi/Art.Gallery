const Advertisement = ({ ImageSrc, LinkUrl }) => {
  return (
    <section className="p-0">
      <div className="container">
        <div className="row">
          <div className="col">
            <a href={LinkUrl} className="d-block card-img-flash">
              <img src={ImageSrc} alt="Advertisement Image" />
            </a>
            <small className="text-end d-block mt-1">تبلیغات</small>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Advertisement;
