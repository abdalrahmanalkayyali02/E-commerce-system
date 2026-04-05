import type { ReactNode } from 'react';
import { motion } from 'framer-motion';
import { features } from '../metadata/FeaturesBar.metadata';
import type { IFeature } from '../../../types/interface/feature.interface';

const containerVariants = {
  hidden: {},
  visible: { transition: { staggerChildren: 0.1 } },
};

const itemVariants = {
  hidden: { opacity: 0, y: 30 },
  visible: {
    opacity: 1,
    y: 0,
    transition: { duration: 0.5, ease: [0.22, 1, 0.36, 1] as const },
  },
};

export function FeaturesBar(): ReactNode {
  return (
    <section className="features-bar" id="features">
      <motion.div
        className="features-bar__grid"
        variants={containerVariants}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true, amount: 0.3 }}
      >
        {features.map((feature: IFeature) => {
          const Icon = feature.icon;
          return (
            <motion.div
              key={feature.id}
              className="feature-item"
              id={feature.id}
              variants={itemVariants}
            >
              <div className="feature-item__icon">
                <Icon size={24} />
              </div>
              <h3 className="feature-item__title">{feature.title}</h3>
              <p className="feature-item__desc">{feature.description}</p>
            </motion.div>
          );
        })}
      </motion.div>
    </section>
  );
}
