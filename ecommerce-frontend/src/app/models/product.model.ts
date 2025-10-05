export interface Product {
  id: number;
  category: string;
  productCode: string;
  name: string;
  imagePath?: string;
  price: number;
  minimumQuantity: number;
  discountRate: number;
}