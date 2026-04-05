/**
 * Enum-like constant for category identifiers.
 */
export const CategoryId = {
  Fashion: 'cat-fashion',
  Electronics: 'cat-electronics',
  Accessories: 'cat-accessories',
} as const;

export type CategoryId = (typeof CategoryId)[keyof typeof CategoryId];
