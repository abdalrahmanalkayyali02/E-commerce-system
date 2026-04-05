/**
 * Props for the NavLink atom component.
 */
export interface INavLinkProps {
  readonly href: string;
  readonly label: string;
  readonly onClick?: () => void;
  readonly className?: string;
}
