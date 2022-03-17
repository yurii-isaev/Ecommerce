export function checkDataType(data, msg) {
  if (Array.isArray(data)) {
    console.log(`Array // ${msg}:`, data);
  } else if (typeof data === 'object' && !Array.isArray(data)) {
    console.log(`Object // ${msg}:`, data.value);
  }
}

export function isEmptyObject(obj) {
  return obj && Object.keys(obj).length === 0 && obj.constructor === Object;
}