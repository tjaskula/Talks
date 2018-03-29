mymin :: (Ord a) => [a] -> a 
mymin [] = error "empty list"
mymin [x] = x  
mymin (x:y:xs) = if x < y then mymin(x:xs) else mymin(y:xs)