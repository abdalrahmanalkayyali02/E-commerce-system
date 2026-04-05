import type { CategoryId } from '../enum/category-id.enum';

/**
 * Represents a product category card on the landing page.
 */
export interface ICategory {
  readonly id: CategoryId;
  readonly title: string;
  readonly subtitle: string;
  readonly count: string;
  readonly image: string;
}
