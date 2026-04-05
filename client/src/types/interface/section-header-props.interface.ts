import type { ReactNode } from 'react';

/**
 * Props for the SectionHeader atom component.
 */
export interface ISectionHeaderProps {
  readonly tag: string;
  readonly title: string;
  readonly subtitle?: string;
  readonly children?: ReactNode;
}
