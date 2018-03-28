public class Math {

  public int min(Integer[] arr){
    min = arr[0];
    for (j=0; j < arr.length; j++) {
        if (arr[j] < min) {
            min = arr[j];
        }
    }
  } 
}