const Badge = ({className, LinkUrl, Text}) => {
  return (
    <a href={LinkUrl} className={className}>
      <i className="fas fa-circle me-2 small fw-bold"></i>
      {Text}
    </a>
  );
};

export default Badge;
