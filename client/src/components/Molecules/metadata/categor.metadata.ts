import { CategoryId } from '../../../types/enum/category-id.enum';
import type { ICategory } from '../../../types/interface/category.interface';
import fashionImg from '../../../assets/category-fashion.png';
import electronicsImg from '../../../assets/category-electronics.png';
import accessoriesImg from '../../../assets/category-accessories.png';

export const categories: readonly ICategory[] = [
  {
    id: CategoryId.Fashion,
    title: 'Fashion',
    subtitle: 'Trending Styles',
    count: '2,400+ Items',
    image: fashionImg,
  },
  {
    id: CategoryId.Electronics,
    title: 'Electronics',
    subtitle: 'Latest Tech',
    count: '1,800+ Items',
    image: electronicsImg,
  },
  {
    id: CategoryId.Accessories,
    title: 'Accessories',
    subtitle: 'Premium Picks',
    count: '3,100+ Items',
    image: accessoriesImg,
  },
] as const;