const CustomButton = ({ Icon, Text, className, Link }) => {
  function CheckIcon() {
    if (Icon) {
      return <i className={Icon + " me-2"}></i>;
    }
  }
  
  return (
    <a href={Link} className={className}>
      {CheckIcon()}
      {Text}
    </a>
  );
};

export default CustomButton;
