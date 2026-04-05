/**
 * Enum-like constant for product badge/tag types.
 */
export const ProductTag = {
  BestSeller: 'Best Seller',
  New: 'New',
  Limited: 'Limited',
} as const;

export type ProductTag = (typeof ProductTag)[keyof typeof ProductTag];
