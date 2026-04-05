import type { ProductTag } from '../enum/product-tag.enum';

/**
 * Represents a product displayed in the featured products grid.
 */
export interface IProduct {
  readonly id: string;
  readonly name: string;
  readonly brand: string;
  readonly price: number;
  readonly originalPrice?: number;
  readonly rating: number;
  readonly reviews: number;
  readonly image: string;
  readonly tag?: ProductTag;
}
