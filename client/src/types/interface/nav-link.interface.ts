import type { NavSection } from '../enum/nav-section.enum';

/**
 * Represents a single navigation link item.
 */
export interface INavLink {
  readonly label: string;
  readonly href: NavSection;
}
