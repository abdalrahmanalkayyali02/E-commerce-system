import { FooterLinkGroup } from '../../../types/enum/footer-link-group.enum';
import type { IFooterLinkGroup } from '../../../types/interface/footer-link-group.interface';

export const footerLinkGroups: readonly IFooterLinkGroup[] = [
  {
    category: FooterLinkGroup.Shop,
    links: ['All Products', 'New Arrivals', 'Best Sellers', 'Sale', 'Gift Cards'],
  },
  {
    category: FooterLinkGroup.Company,
    links: ['About Us', 'Careers', 'Press', 'Sustainability', 'Affiliates'],
  },
  {
    category: FooterLinkGroup.Support,
    links: ['Help Center', 'Shipping Info', 'Returns', 'Order Tracking', 'Size Guide'],
  },
  {
    category: FooterLinkGroup.Legal,
    links: ['Privacy Policy', 'Terms of Service', 'Cookie Policy', 'Accessibility'],
  },
] as const;
