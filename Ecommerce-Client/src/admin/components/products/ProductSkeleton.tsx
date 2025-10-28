import Skeleton from "@mui/material/Skeleton";
import Box from '@mui/material/Box';

export default function ProductSkeleton() {
  return (
    <div>
      <Box sx={{ width: 300 }}>
        <Skeleton />
        <Skeleton animation="wave" />
        <Skeleton animation={false} />
      </Box>
    </div>
  );
}
