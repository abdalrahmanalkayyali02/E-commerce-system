import type { ReactNode } from 'react';
import { motion } from 'framer-motion';
import { ArrowUpRight } from 'lucide-react';
import { categories } from '../metadata/categor.metadata';
import type { ICategory } from '../../../types/interface/category.interface';

const containerVariants = {
  hidden: {},
  visible: {
    transition: { staggerChildren: 0.15 },
  },
};

const cardVariants = {
  hidden: { opacity: 0, y: 40 },
  visible: {
    opacity: 1,
    y: 0,
    transition: { duration: 0.6, ease: [0.22, 1, 0.36, 1] as const },
  },
};

export function CategoryShowcase(): ReactNode {
  return (
    <section className="categories" id="categories">
      <div className="categories__header">
        <motion.span
          className="section-tag"
          initial={{ opacity: 0 }}
          whileInView={{ opacity: 1 }}
          viewport={{ once: true }}
        >
          Browse Categories
        </motion.span>
        <motion.h2
          className="section-title"
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.1 }}
        >
          Shop by Category
        </motion.h2>
        <motion.p
          className="section-subtitle"
          initial={{ opacity: 0, y: 20 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true }}
          transition={{ delay: 0.2 }}
        >
          Explore our curated collections across your favorite categories
        </motion.p>
      </div>

      <motion.div
        className="categories__grid"
        variants={containerVariants}
        initial="hidden"
        whileInView="visible"
        viewport={{ once: true, amount: 0.2 }}
      >
        {categories.map((category: ICategory) => (
          <motion.div
            key={category.id}
            className="category-card"
            id={category.id}
            variants={cardVariants}
            whileHover={{ y: -8 }}
            transition={{ type: 'spring', stiffness: 300, damping: 20 }}
          >
            <a href="#" className="category-card__link">
              <div className="category-card__image-wrapper">
                <img
                  src={category.image}
                  alt={category.title}
                  className="category-card__image"
                />
                <div className="category-card__overlay" />
              </div>
              <div className="category-card__content">
                <div>
                  <span className="category-card__subtitle">{category.subtitle}</span>
                  <h3 className="category-card__title">{category.title}</h3>
                  <span className="category-card__count">{category.count}</span>
                </div>
                <div className="category-card__arrow">
                  <ArrowUpRight size={20} />
                </div>
              </div>
            </a>
          </motion.div>
        ))}
      </motion.div>
    </section>
  );
}
