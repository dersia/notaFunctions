function isValidPostalCode(postalCode, countryCode) {
   var postalCodeRegex;
      switch (countryCode) {
          case "US":
              postalCodeRegex = /^([0-9]{5})(?:[-\s]*([0-9]{4}))?$/.test(postalCode);
              break;
          case "CA":
              postalCodeRegex = /^([A-Z][0-9][A-Z])\s*([0-9][A-Z][0-9])$/.test(postalCode);
              break;
          default:
              return true;
              break;

  }
  return postalCodeRegex;
};

module.exports = isValidPostalCode;