export function randomNumberGenerator(productLength: number) {
  if (productLength === 0) return 1;
  const randomNumber = Math.random() * productLength;
  return Math.floor(randomNumber);
}
