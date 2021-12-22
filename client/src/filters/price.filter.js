function formattoFixedPrice(value) {
   value = parseFloat(value);
   return value.toFixed(2) + ' ₽'
}

function formatPriceWithSpaces(value) {
   let parts = value.toString().split(".");
   parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, " ");
   return parts.join(".");
}

export function formatPrice(price) {
   const formattedPrice = formattoFixedPrice(price);
   return formatPriceWithSpaces(formattedPrice);
}