The trainer used in this project is SdcaMaximumEntropy trainer which is based on Stochastic Dual Coordinate Ascent and uses empirical risk minimization which optimizes based on the training data.

The sampledata.csv file contains six rows of random data. Feel free to adjust the data to fit your own observation or to adjust the trained model. 
Each of these rows contains the value for the properties in the Email Class.

1) Subject
2) Body
3) Sender
4) Category


Here is the snippet of the data:

![image](https://github.com/user-attachments/assets/9026b186-f7ca-43c1-ad81-a6839bb2eb44)


The testdata.csv file contains additional data points to test the trained and evaluate:

![image](https://github.com/user-attachments/assets/bc65724d-baf7-4382-8443-5d40986a25d1)


Run the Console Application with commandline arguments:

1. Train and test-evaluate the model using sampledata.csv and testdata.csv
> D:\Machine Learning Projects\Email-MultiClass-Classification\bin\Debug\net8.0 train
> "D:\Machine Learning Projects\Email-MultiClass-Classification\Data\sampledata.csv" 
> "D:\Machine Learning Projects\Email-MultiClass-Classification\Data\testdata.csv"


Output:

![image](https://github.com/user-attachments/assets/47e940e5-c92e-41f1-acd0-b8fa59825c9e)


2. After training the model, build a sample JSON file and save it as input.json as follows:

![image](https://github.com/user-attachments/assets/49da1d70-41b3-4b47-b2c7-40fa3fa3b882)

3. To run the model with the input.json, simply pass in the filename to built application and the predicted output will appear:



>  D:\Machine Learning Projects\Email-MultiClass-Classification\bin\Debug\net8.0\CarPricePrediction.exe predict 
> "D:\Machine Learning Projects\Email-MultiClass-Classification\Data\input.json"

![image](https://github.com/user-attachments/assets/24c6527c-dc0b-4831-ac4e-3e2c38c5f204)





