import type { ReactNode } from 'react';

/**
 * Props for the IconButton atom component.
 */
export interface IIconButtonProps {
  readonly id?: string;
  readonly ariaLabel: string;
  readonly onClick?: () => void;
  readonly children: ReactNode;
  readonly className?: string;
  readonly badge?: number;
}
