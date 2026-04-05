import type { ReactNode } from 'react';
import type { ISectionHeaderProps } from '../../types/interface/section-header-props.interface';

export function SectionHeader({ tag, title, subtitle }: ISectionHeaderProps): ReactNode {
  return (
    <div className="section-header">
      <span className="section-tag">{tag}</span>
      <h2 className="section-title">{title}</h2>
      {subtitle !== undefined && <p className="section-subtitle">{subtitle}</p>}
    </div>
  );
}
