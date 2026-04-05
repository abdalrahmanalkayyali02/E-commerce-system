import { NavSection } from '../../../types/enum/nav-section.enum';
import type { INavLink } from '../../../types/interface/nav-link.interface';

export const navLinks: readonly INavLink[] = [
  { label: 'Home', href: NavSection.Home },
  { label: 'Shop', href: NavSection.Shop },
  { label: 'Collections', href: NavSection.Collections },
  { label: 'About', href: NavSection.About },
  { label: 'Contact', href: NavSection.Contact },
] as const;
