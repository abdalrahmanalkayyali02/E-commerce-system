import type { FooterLinkGroup } from '../enum/footer-link-group.enum';

/**
 * Represents a group of footer links under a specific category.
 */
export interface IFooterLinkGroup {
  readonly category: FooterLinkGroup;
  readonly links: readonly string[];
}
