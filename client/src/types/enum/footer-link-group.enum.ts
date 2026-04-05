/**
 * Enum-like constant for footer link group categories.
 */
export const FooterLinkGroup = {
  Shop: 'Shop',
  Company: 'Company',
  Support: 'Support',
  Legal: 'Legal',
} as const;

export type FooterLinkGroup = (typeof FooterLinkGroup)[keyof typeof FooterLinkGroup];
